import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {Piece} from "../../../models/piece";
import {PieceService} from "../../services/piece.service";

@Injectable({
  providedIn: 'root'
})
export class PieceResolver implements Resolve<Piece> {
  constructor(private pieceService: PieceService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Piece> {
    return this.pieceService.getPiece(+route.paramMap.get('id')!);
  }
}
