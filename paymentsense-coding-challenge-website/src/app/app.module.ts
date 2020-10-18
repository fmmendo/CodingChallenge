import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { PaymentsenseCodingChallengeApiService } from "./services";
import { HttpClientModule } from "@angular/common/http";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { CountriesComponent } from "./countries/countries.component";
import { MatTableModule, MatPaginatorModule } from "@angular/material";
import { PluckPipe } from "./pluck";
import { PluckJoinPipe } from "./pluckjoin";
import { CurrencyPipe } from "./currencyPipe";

@NgModule({
  declarations: [
    AppComponent,
    CountriesComponent,
    PluckPipe,
    PluckJoinPipe,
    CurrencyPipe,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FontAwesomeModule,
    // NgxPaginationModule,
    MatTableModule,
    MatPaginatorModule,
  ],
  providers: [PaymentsenseCodingChallengeApiService],
  bootstrap: [AppComponent],
})
export class AppModule {}
