import {Component} from '@angular/core';
import {RouterModule} from '@angular/router';
import {TableLazyLoadEvent, TableModule} from 'primeng/table';
import {CardModule} from 'primeng/card';
import {TooltipModule} from 'primeng/tooltip';
import {ButtonModule} from 'primeng/button';
import {ClientDto} from '@core/models';
import {ApiService} from '@core/services';
import {SortUtils} from '@shared/utils';

@Component({
  selector: 'app-clients-list',
  standalone: true,
  templateUrl: './clients-list.component.html',
  imports: [
    ButtonModule,
    TooltipModule,
    RouterModule,
    CardModule,
    TableModule,
  ],
})
export class ClientsListComponent {
  public clients: ClientDto[] = [];
  public isLoading = true;
  public totalRecords = 0;
  public first = 0;

  constructor(private readonly apiService: ApiService) {}

  search(event: Event) {
    this.isLoading = true;
    const searchValue = (event.target as HTMLInputElement).value;

    this.apiService.getClients({search: searchValue}).subscribe((result) => {
      if (result.isSuccess) {
        this.clients = result.data;
        this.totalRecords = result.totalItems;
      }

      this.isLoading = false;
    });
  }

  load(event: TableLazyLoadEvent) {
    this.isLoading = true;
    const first = event.first ?? 1;
    const rows = event.rows ?? 10;
    const page = first / rows + 1;
    const sortField = SortUtils.parseSortProperty(event.sortField as string, event.sortOrder);

    this.apiService.getClients({orderBy: sortField, page: page, pageSize: rows}).subscribe((result) => {
      if (result.isSuccess) {
        this.clients = result.data;
        this.totalRecords = result.totalItems;
      }

      this.isLoading = false;
    });
  }
}
