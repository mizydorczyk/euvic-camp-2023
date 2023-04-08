import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {RankingComponent} from './ranking/ranking.component';
import {NavbarComponent} from "./navbar/navbar.component";
import {PieceDetailsComponent} from './piece-details/piece-details.component';
import {SignInComponent} from './sign-in/sign-in.component';
import {SignUpComponent} from './sign-up/sign-up.component';
import {ReactiveFormsModule} from "@angular/forms";
import {EmailConfirmedComponent} from './email-confirmed/email-confirmed.component';
import {ToastrModule} from "ngx-toastr";
import {ErrorInterceptor} from "./interceptors/error.interceptor";
import {BroadcastListComponent} from "./broadcast-list/broadcast-list.component";

@NgModule({
  declarations: [
    AppComponent,
    RankingComponent,
    NavbarComponent,
    PieceDetailsComponent,
    SignInComponent,
    SignUpComponent,
    EmailConfirmedComponent,
    BroadcastListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    }),
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
