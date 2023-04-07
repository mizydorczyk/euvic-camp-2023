import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RankingComponent} from "./ranking/ranking.component";
import {BroadcastListComponent} from "./broadcast-list/broadcast-list.component";
import {PieceDetailsComponent} from "./piece-details/piece-details.component";
import {SignInComponent} from "./sign-in/sign-in.component";
import {SignUpComponent} from "./sign-up/sign-up.component";

const routes: Routes = [
  {path: 'ranking', component: RankingComponent},
  {path: 'broadcast-list', component: BroadcastListComponent},
  {path: 'broadcast-list:id', component: PieceDetailsComponent},
  {path: 'sign-in', component: SignInComponent},
  {path: 'sign-up', component: SignUpComponent},
  {path: '**', redirectTo: 'ranking', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    onSameUrlNavigation: 'reload'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
