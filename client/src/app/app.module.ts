import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
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
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {EmailConfirmedComponent} from './email-confirmed/email-confirmed.component';
import {ToastrModule} from "ngx-toastr";
import {ErrorInterceptor} from "./interceptors/error.interceptor";
import {BroadcastListComponent} from "./broadcast-list/broadcast-list.component";
import {IsInRoleDirective} from './shared/directives/is-in-role.directive';
import {UsersComponent} from './users/users.component';
import {UserFormComponent} from './shared/user-form/user-form.component';
import {UpdateUserComponent} from './update-user/update-user.component';
import {DurationPipe} from './shared/pipes/duration.pipe';
import {NumberSuffixPipe} from './shared/pipes/number-suffix.pipe';
import {NgxSpinnerModule} from "ngx-spinner";
import {LoadingInterceptor} from "./interceptors/loading.interceptor";
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import {PaginationModule} from "ngx-bootstrap/pagination";
import {NgChartsModule} from "ng2-charts";

@NgModule({
  declarations: [
    AppComponent,
    RankingComponent,
    NavbarComponent,
    PieceDetailsComponent,
    SignInComponent,
    SignUpComponent,
    EmailConfirmedComponent,
    BroadcastListComponent,
    IsInRoleDirective,
    UsersComponent,
    UserFormComponent,
    UpdateUserComponent,
    DurationPipe,
    NumberSuffixPipe
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
    NgxSpinnerModule.forRoot({type: 'line-scale'}),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    FormsModule,
    NgChartsModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class AppModule {
}
