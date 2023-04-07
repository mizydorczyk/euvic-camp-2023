import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {HttpClientModule} from "@angular/common/http";
import { RankingComponent } from './ranking/ranking.component';
import {NavbarComponent} from "./navbar/navbar.component";
import { PieceDetailsComponent } from './piece-details/piece-details.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import {ReactiveFormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    RankingComponent,
    NavbarComponent,
    PieceDetailsComponent,
    SignInComponent,
    SignUpComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
