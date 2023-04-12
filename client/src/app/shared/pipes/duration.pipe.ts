import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'duration'
})
export class DurationPipe implements PipeTransform {
  transform(value: string): string {
    let values = value.split(':');
    let totalSeconds = (+values[0]) * 3600 + (+values[1]) * 60 + (+values[2]);

    const minutes = (totalSeconds - totalSeconds % 60) / 60;
    const seconds = totalSeconds - 60 * minutes;

    return `${minutes} minutes ${seconds} seconds`;
  }
}
