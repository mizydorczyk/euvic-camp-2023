import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Piece} from "../../models/piece";
import {map, of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PieceService {
  baseUrl = environment.apiUrl;
  ranking: Piece[] = [];

  constructor(private http: HttpClient) {
  }

  getRanking() {
    if (this.ranking.length > 0) return of(this.ranking);
    return this.http.get<Piece[]>(this.baseUrl + 'piece/top100', {withCredentials: true}).pipe(
      map(pieces => {
          this.ranking = pieces;
          return pieces;
        }
      ));
  }

  getPiece(id: number) {
    return this.http.get<Piece>(this.baseUrl + 'piece/' + id, {withCredentials: true}).pipe(
      map(piece => {
        return piece;
      })
    );
  }
}
