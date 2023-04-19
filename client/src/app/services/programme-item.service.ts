import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {ProgrammeItem} from "../../models/programmeItem";
import {map, of} from "rxjs";
import {PagedResult} from "../../models/pagedResult";
import {QueryParams} from "../../models/queryParams";
import {SortingOption} from "../../models/sortingOption";

@Injectable({
  providedIn: 'root'
})
export class ProgrammeItemService {
  baseUrl = environment.apiUrl;
  programmeItemsResult?: PagedResult<ProgrammeItem>;
  programmeItemsCache = new Map<string, PagedResult<ProgrammeItem>>();

  currentSortingOption: SortingOption = {name: 'Title: A-Z', value: 'Title'};

  constructor(private http: HttpClient) {
  }

  getProgrammeItems(params: QueryParams) {
    if (this.programmeItemsCache.size > 0) {
      if (this.programmeItemsCache.has(Object.values(params).join('-'))) {
        this.programmeItemsResult = this.programmeItemsCache.get(Object.values(params).join('-'));
        if (this.programmeItemsResult) return of(this.programmeItemsResult);
      }
    }

    return this.http.get<PagedResult<ProgrammeItem>>(this.baseUrl + 'programmeItems', {
      params: params as any,
      withCredentials: true
    }).pipe(
      map(result => {
          this.programmeItemsCache.set(Object.values(params).join('-'), result);
          this.programmeItemsResult = result;
          return this.programmeItemsResult;
        }
      ));
  }

  downloadProgrammeItems(params: QueryParams) {
    return this.http.get(this.baseUrl + 'programmeItems/export', {
      params: params as any,
      withCredentials: true,
      responseType: "blob"
    });
  }
}
