using System.Globalization;
using API.Helpers;
using API.Models;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Sieve.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User, Admin")]
public class ProgrammeItemsController : ControllerBase
{
    private readonly IProgrammeItemsRepository _programmeItemsRepository;

    public ProgrammeItemsController(IProgrammeItemsRepository programmeItemsRepository)
    {
        _programmeItemsRepository = programmeItemsRepository;
    }

    [Cache(300)]
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProgrammeItemDto>>> GetProgrammeItems([FromQuery] SieveModel query)
    {
        var result = await _programmeItemsRepository.ListAsync(query);
        var programmeItems = result.Items.Select(programmeItem => new ProgrammeItemDto
        {
            PlaybackDate = programmeItem.PlaybackDate,
            BroadcastingDuration = programmeItem.BroadcastingDuration,
            RadioChannel = new RadioChannelDto
            {
                Name = programmeItem.RadioChannel.Name,
                Frequency = programmeItem.RadioChannel.Frequency
            },
            Piece = new PieceDto
            {
                Id = programmeItem.Piece.Id,
                Title = programmeItem.Piece.Title,
                ReleaseDate = programmeItem.Piece.ReleaseDate,
                Duration = programmeItem.Piece.Duration,
                Version = programmeItem.Piece.Version,
                Artist = programmeItem.Piece.Artist.KnownAs,
                Views = programmeItem.Piece.ProgrammeItems.Sum(x => x.Views)
            },
            Views = programmeItem.Views
        }).ToList();

        return Ok(new PagedResult<ProgrammeItemDto>(programmeItems, result.TotalCount, query.PageSize.Value, query.Page.Value));
    }

    [Cache(300)]
    [HttpGet("export")]
    public async Task<FileResult> ExportProgrammeItems([FromQuery] SieveModel query)
    {
        query.Page = null;
        query.PageSize = null;

        var result = await _programmeItemsRepository.ListAsync(query);

        var memoryStream = new MemoryStream();

        using (var package = new ExcelPackage(memoryStream))
        {
            var worksheet = package.Workbook.Worksheets.Add("broadcast-list");
            worksheet.Name = "broadcast-list";

            worksheet.Cells[1, 1].Value = "Title";
            worksheet.Cells[1, 2].Value = "Version";
            worksheet.Cells[1, 3].Value = "Artist";
            worksheet.Cells[1, 4].Value = "Release date";
            worksheet.Cells[1, 5].Value = "Piece duration";
            worksheet.Cells[1, 6].Value = "Radio channel name";
            worksheet.Cells[1, 7].Value = "Radio channel frequency";
            worksheet.Cells[1, 8].Value = "Playback date";
            worksheet.Cells[1, 9].Value = "Broadcasting duration";
            worksheet.Cells[1, 10].Value = "Views";

            worksheet.Cells["A1:J1"].Style.Font.Bold = true;

            for (var i = 0; i < result.Items.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = result.Items[i].Piece.Title;
                worksheet.Cells[i + 2, 2].Value = result.Items[i].Piece.Version;
                worksheet.Cells[i + 2, 3].Value = result.Items[i].Piece.Artist.KnownAs;
                worksheet.Cells[i + 2, 4].Value = result.Items[i].Piece.ReleaseDate.ToString(CultureInfo.CurrentCulture);
                worksheet.Cells[i + 2, 5].Value = result.Items[i].Piece.Duration.ToString();
                worksheet.Cells[i + 2, 6].Value = result.Items[i].RadioChannel.Name;
                worksheet.Cells[i + 2, 7].Value = result.Items[i].RadioChannel.Frequency;
                worksheet.Cells[i + 2, 8].Value = result.Items[i].PlaybackDate.ToString(CultureInfo.CurrentCulture);
                worksheet.Cells[i + 2, 9].Value = result.Items[i].BroadcastingDuration.ToString();
                worksheet.Cells[i + 2, 10].Value = result.Items[i].Views;
            }

            await package.SaveAsync();
        }

        memoryStream.Position = 0;
        const string contentType = "application/octet-stream";
        const string fileName = "broadcast-list.xlsx";
        return File(memoryStream, contentType, fileName);
    }
}