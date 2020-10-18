import { Pipe, PipeTransform } from "@angular/core";
import { Currency } from "./model/currency";

@Pipe({ name: "currency" })
export class CurrencyPipe implements PipeTransform {
  transform(input: Currency[]): any {
    return input.map((c) => `${c.name} (${c.symbol}) ${c.code}`).join(", ");
  }
}
