import {Component, OnInit, ViewChild} from '@angular/core';
import {PieceService} from "../services/piece.service";
import {Piece} from "../../models/piece";
import DatalabelsPlugin from 'chartjs-plugin-datalabels';
import {default as Annotation} from 'chartjs-plugin-annotation';
import {ActivatedRoute} from "@angular/router";
import {Chart, ChartConfiguration, ChartData, ChartType, LinearScale, LineElement, Tooltip} from "chart.js";
import {BaseChartDirective} from "ng2-charts";

@Component({
  selector: 'app-piece-details',
  templateUrl: './piece-details.component.html',
  styleUrls: ['./piece-details.component.scss']
})
export class PieceDetailsComponent implements OnInit {
  @ViewChild('pieChart') pieChart: (BaseChartDirective | undefined);
  @ViewChild('lineChart') lineChart: (BaseChartDirective | undefined);
  piece?: Piece;
  radioChannelsData: { name: string, views: number }[] = [];
  viewsOvertimeData: { views: number, date: string }[] = [];

  // line chart
  lineChartData: ChartConfiguration['data'] = {
    datasets: [
      {
        data: [] as number[]
      }
    ],
    labels: [] as string[]
  };
  lineChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      y:
        {
          beginAtZero: true,
          position: 'left',
        }
    },
    plugins: {
      legend: {
        display: false,
      }
    }
  };
  lineChartType: ChartType = 'bar';

  // pie chart
  pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: false,
      },
      datalabels: {
        color: 'rgba(0, 0, 0, 255)',
        formatter: (value, ctx) => {
          if (ctx.chart.data.labels) {
            return ctx.chart.data.labels[ctx.dataIndex];
          }
        },
        font: {
          weight: 'bold',
          size: 16,
        }
      },
    }
  };
  pieChartData: ChartData<'pie', number[], string | string[]> = {
    labels: [] as string[],
    datasets: [{
      data: [] as number[]
    }]
  };
  pieChartType: ChartType = 'pie';
  pieChartPlugins = [DatalabelsPlugin];

  constructor(private pieceService: PieceService, private route: ActivatedRoute) {
    Chart.register(Annotation, LinearScale, LineElement, Tooltip);
  }

  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => {
        this.piece = data['piece'];

        if (this.piece?.programmeItemHeaders) {
          const programmeItemHeaders = this.piece.programmeItemHeaders;
          for (let i = 0; i < programmeItemHeaders.length; i++) {

            // data for pie chart
            const radioChannelDataItemIndex = this.radioChannelsData.findIndex(x => x.name === programmeItemHeaders[i].radioChannel.name)
            if (radioChannelDataItemIndex == -1) {
              this.radioChannelsData.push(
                {
                  name: programmeItemHeaders[i].radioChannel.name,
                  views: programmeItemHeaders[i].views
                }
              );
            } else {
              this.radioChannelsData[radioChannelDataItemIndex].views += programmeItemHeaders[i].views;
            }

            // data for line chart
            const viewsOvertimeDataItemIndex = this.viewsOvertimeData.findIndex(x => x.date === programmeItemHeaders[i].playbackDate.substring(0, programmeItemHeaders[i].playbackDate.indexOf('T')))
            if (viewsOvertimeDataItemIndex == -1) {
              this.viewsOvertimeData.push({
                views: programmeItemHeaders[i].views,
                date: programmeItemHeaders[i].playbackDate.substring(0, programmeItemHeaders[i].playbackDate.indexOf('T'))
              });
            } else {
              this.viewsOvertimeData[viewsOvertimeDataItemIndex].views += programmeItemHeaders[i].views;
            }
          }
        }


        this.pieChartData.labels = this.radioChannelsData.map(x => x.name);
        this.pieChartData.datasets.pop();
        this.pieChartData.datasets.push({data: this.radioChannelsData.map(x => x.views)});

        this.lineChartData.labels = this.viewsOvertimeData.map(x => x.date);
        this.lineChartData.datasets.pop();
        this.lineChartData.datasets.push({
          data: this.viewsOvertimeData.map(x => x.views),
          label: 'Views per day'
        });
      }
    })
  }

  onResize() {
    this.lineChart?.chart?.render();
    this.pieChart?.chart?.render();
  }
}
