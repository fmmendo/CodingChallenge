import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "pluckjoin" })
export class PluckJoinPipe implements PipeTransform {
  transform(input: any[], key: string): string {
    return input.map((value) => value[key]).join(", ");
  }
}
