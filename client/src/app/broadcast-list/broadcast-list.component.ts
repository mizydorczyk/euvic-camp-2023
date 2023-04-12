import {Component, OnInit} from '@angular/core';
import {take} from "rxjs";
import {ProgrammeItem} from "../../models/programmeItem";
import {ProgrammeItemService} from "../services/programme-item.service";
import {QueryParams} from "../../models/queryParams";

@Component({
  selector: 'app-broadcast-list',
  templateUrl: './broadcast-list.component.html',
  styleUrls: ['./broadcast-list.component.scss']
})
export class BroadcastListComponent implements OnInit {
  programmeItems: ProgrammeItem[] = []
  params: QueryParams = {
    filters: "",
    page: 1,
    pageSize: 100,
    sorts: "playbackDate"
  }

  constructor(private programmeItemsService: ProgrammeItemService) {
  }

  ngOnInit(): void {
    this.programmeItemsService.getProgrammeItems(this.params).pipe(take(1)).subscribe({
      next: programmeItems => {
        this.programmeItems = programmeItems;
      }
    });
  }
}
