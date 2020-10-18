import { Currency } from "./currency";
import { Language } from "./languange";

export class Country {
  name: string;
  alpha2Code: string;
  alpha3Code: string;
  capital: string;
  altSpellings: string[];
  region: string;
  population: string;
  area: string;
  timezones: string[];
  borders: string[];
  nativeName: string;
  currencies: Currency[];
  languages: Language[];
  flag: string;
}
