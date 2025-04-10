import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/Constants';

@Pipe({
  name: 'DateTimeFormartPipe'
})
export class DateTimeFormartPipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value,Constants.DATE_TIME_FMT);
  }

}
