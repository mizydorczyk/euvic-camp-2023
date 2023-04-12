import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Piece} from "../../models/piece";
import {map, of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RankingService {
  baseUrl = environment.apiUrl;
  pieces: Piece[] = [];

  constructor(private http: HttpClient) {
  }

  getRanking() {
    if (this.pieces.length > 0) return of(this.pieces);
    return this.http.get<Piece[]>(this.baseUrl + 'piece/top100', {withCredentials: true}).pipe(
      map(pieces => {
          this.pieces = pieces;
          return pieces;
        }
      ));
  }
}
