import {Pipe, PipeTransform} from '@angular/core';
import * as moment from "moment/moment";

@Pipe({
  name: 'duration'
})
export class DurationPipe implements PipeTransform {
  transform(value: string): string {
    let totalSeconds = moment.duration(value).asSeconds();

    const minutes = (totalSeconds - totalSeconds % 60) / 60;
    const seconds = totalSeconds - 60 * minutes;

    return `${minutes} minutes ${seconds} seconds`;
  }
}
