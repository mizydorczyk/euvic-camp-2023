import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RankingComponent} from "./ranking/ranking.component";
import {BroadcastListComponent} from "./broadcast-list/broadcast-list.component";
import {PieceDetailsComponent} from "./piece-details/piece-details.component";
import {SignInComponent} from "./sign-in/sign-in.component";
import {SignUpComponent} from "./sign-up/sign-up.component";
import {EmailConfirmedComponent} from "./email-confirmed/email-confirmed.component";
import {AdminGuard} from "./guards/admin.guard";
import {AuthGuard} from "./guards/auth.guard";
import {UsersComponent} from "./users/users.component";
import {UpdateUserComponent} from "./update-user/update-user.component";

const routes: Routes = [
  {
    path: '', runGuardsAndResolvers: 'always', children: [
      {path: 'ranking', component: RankingComponent, canActivate: [AuthGuard],},
      {path: 'broadcast-list', component: BroadcastListComponent, canActivate: [AuthGuard]},
      {path: 'broadcast-list/:id', component: PieceDetailsComponent, canActivate: [AuthGuard]},
      {path: 'sign-in', component: SignInComponent},
      {path: 'sign-up', component: SignUpComponent},
      {path: 'register-new-user', component: SignUpComponent, canActivate: [AdminGuard]},
      {path: 'update-user', component: UpdateUserComponent, canActivate: [AdminGuard]},
      {path: 'email-confirmed', component: EmailConfirmedComponent},
      {path: 'users', component: UsersComponent, canActivate: [AdminGuard]},
    ],
  },
  {path: '**', redirectTo: 'ranking', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
