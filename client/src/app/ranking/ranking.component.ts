import {Component, OnInit} from '@angular/core';
import {PieceService} from "../services/piece.service";
import {take} from "rxjs";
import {Piece} from "../../models/piece";

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.scss']
})
export class RankingComponent implements OnInit {
  pieces: Piece[] = []

  constructor(private pieceService: PieceService) {
  }

  ngOnInit(): void {
    this.pieceService.getRanking().pipe(take(1)).subscribe({
      next: pieces => {
        this.pieces = pieces;
      }
    });
  }
}
