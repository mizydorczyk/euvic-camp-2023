import {Component, OnInit} from '@angular/core';
import {RankingService} from "../services/ranking.service";
import {take} from "rxjs";
import {Piece} from "../../models/piece";

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.scss']
})
export class RankingComponent implements OnInit {
  pieces: Piece[] = []

  constructor(private rankingService: RankingService) {
  }

  ngOnInit(): void {
    this.rankingService.getRanking().pipe(take(1)).subscribe({
      next: pieces => {
        this.pieces = pieces;
      }
    });
  }
}
