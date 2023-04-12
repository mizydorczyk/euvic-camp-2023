import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {ProgrammeItem} from "../../models/programmeItem";
import {map, of} from "rxjs";
import {PagedResult} from "../../models/pagedResult";

@Injectable({
  providedIn: 'root'
})
export class ProgrammeItemService {
  baseUrl = environment.apiUrl;
  programmeItems: ProgrammeItem[] = [];

  constructor(private http: HttpClient) {
  }

  getProgrammeItems(params: any) {
    if (this.programmeItems.length > 0) return of(this.programmeItems);
    return this.http.get<PagedResult<ProgrammeItem>>(this.baseUrl + 'programmeItems', {
      params: params,
      withCredentials: true
    }).pipe(
      map(result => {
          this.programmeItems = result.items;
          return this.programmeItems;
        }
      ));
  }
}
