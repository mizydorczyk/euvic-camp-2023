import {Component, OnInit} from '@angular/core';
import {take} from "rxjs";
import {ProgrammeItem} from "../../models/programmeItem";
import {ProgrammeItemService} from "../services/programme-item.service";
import {QueryParams} from "../../models/queryParams";
import {BsDropdownConfig} from "ngx-bootstrap/dropdown";
import {SortingOption} from "../../models/sortingOption";
import {PagedResult} from "../../models/pagedResult";
import {saveAs} from 'file-saver';

@Component({
  selector: 'app-broadcast-list',
  templateUrl: './broadcast-list.component.html',
  styleUrls: ['./broadcast-list.component.scss'],
  providers: [{provide: BsDropdownConfig, useValue: {autoClose: true}}]
})
export class BroadcastListComponent implements OnInit {
  searchContent = '';

  programmeItemsResult?: PagedResult<ProgrammeItem>;
  params: QueryParams = {
    filters: '',
    page: 1,
    pageSize: 10,
    sorts: ''
  }
  sortingOptions: SortingOption[] = [
    {name: 'Title: A-Z', value: 'Title'},
    {name: 'Title: Z-A', value: '-Title'},
    {name: 'Radio channel: A-Z', value: 'RadioChannelName'},
    {name: 'Radio channel: Z-A', value: '-RadioChannelName'},
    {name: 'Playback date: from newest', value: '-PlaybackDate'},
    {name: 'Playback date: from oldest', value: 'PlaybackDate'},
    {name: 'Broadcasting duration: from shortest', value: 'BroadcastingDuration'},
    {name: 'Broadcasting duration: from longest', value: '-BroadcastingDuration'}
  ]

  constructor(public programmeItemsService: ProgrammeItemService) {
  }

  onSortSelected(option: any) {
    this.programmeItemsService.currentSortingOption = option;
    this.params.sorts = option.value;
    this.params.page = 1;
    this.getProgrammeItems();
  }

  ngOnInit(): void {
    this.params.sorts = this.programmeItemsService.currentSortingOption.value;
    this.getProgrammeItems();
  }

  getProgrammeItems() {
    this.programmeItemsService.getProgrammeItems(this.params).pipe(take(1)).subscribe({
      next: programmeItemsResult => {
        this.programmeItemsResult = programmeItemsResult
      }
    });
  }

  onPageChanged(event: any) {
    if (this.params.page !== event.page) {
      this.params.page = event.page;
      this.getProgrammeItems();
      window.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
      });
    }
  }

  downloadProgrammeItems() {
    this.programmeItemsService.downloadProgrammeItems(this.params).pipe(take(1)).subscribe({
      next: data => {
        let blob = new Blob([data], {type: "application/vnd.ms-excel"});
        saveAs(blob, "broadcast-list-export.xlsx");
      }
    });
  }

  search() {
    this.params.filters = `Title@=*${this.searchContent}`;
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
    this.getProgrammeItems();
  }
}
