import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Piece} from "../../models/piece";

@Injectable({
  providedIn: 'root'
})
export class RankingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getRanking() {
    return this.http.get<Piece[]>(this.baseUrl + 'piece/top100', {withCredentials: true});
  }
}
