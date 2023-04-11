import {Component, OnInit} from '@angular/core';
import {RankingService} from "../services/ranking.service";
import {Piece} from "../../models/piece";
import {locale} from "moment";

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.scss']
})
export class RankingComponent implements OnInit {
  pieces: Piece[] = [];
  protected readonly locale = locale;

  constructor(private rankingService: RankingService) {
  }

  ngOnInit(): void {
    this.loadRanking();
  }

  loadRanking() {
    this.rankingService.getRanking().subscribe({
      next: pieces => this.pieces = pieces
    })
  }
}
