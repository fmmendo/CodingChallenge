import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { take } from "rxjs/operators";
import { Country } from "../model/country";
import { PaymentsenseCodingChallengeApiService } from "../services";
import { MatTableDataSource } from "@angular/material/table";
import { MatPaginator } from "@angular/material/paginator";
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from "@angular/animations";

@Component({
  selector: "app-countries",
  templateUrl: "./countries.component.html",
  styleUrls: ["./countries.component.scss"],
  animations: [
    trigger("detailExpand", [
      state("collapsed", style({ height: "0px", minHeight: "0" })),
      state("expanded", style({ height: "*" })),
      transition(
        "expanded <=> collapsed",
        animate("225ms cubic-bezier(0.4, 0.0, 0.2, 1)")
      ),
    ]),
  ],
})
export class CountriesComponent implements AfterViewInit, OnInit {
  // public countries: Country[];
  // error: string;
  countriesList = new MatTableDataSource<Country>();
  columnsToDisplay: string[] = ["flag", "name"];
  expandedCountry: Country | null;
  // isExpandedRow = (i: number, row: Object) => row.hasOwnProperty("detailRow");

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(
    private paymentsenseCodingChallengeApiService: PaymentsenseCodingChallengeApiService
  ) {
    paymentsenseCodingChallengeApiService.getCountries().subscribe(
      (response) => {
        // this.countries = response;
        this.countriesList = new MatTableDataSource<Country>(response);
        this.countriesList.data = response;
        this.countriesList.paginator = this.paginator;
      },
      (_) => {
        // this.countries = null;
        // this.error = "Unable to fetch countries.";
      }
    );
  }
  ngAfterViewInit(): void {
    // this.countriesList.paginator = this.paginator;
  }

  rowClick(row: any) {
    this.expandedCountry = this.expandedCountry === row ? null : row;
    console.log(this.expandedCountry);
  }

  ngOnInit() {}
}
