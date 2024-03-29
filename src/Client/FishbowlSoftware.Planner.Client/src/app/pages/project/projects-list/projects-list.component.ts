import {Component} from '@angular/core';
import {RouterModule} from '@angular/router';
import {TableLazyLoadEvent, TableModule} from 'primeng/table';
import {CardModule} from 'primeng/card';
import {TooltipModule} from 'primeng/tooltip';
import {ButtonModule} from 'primeng/button';
import {ConfirmationService} from 'primeng/api';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {ProjectDto} from '@core/models';
import {ApiService, ToastService} from '@core/services';
import {SortUtils} from '@shared/utils';

@Component({
  selector: 'app-projects-list',
  standalone: true,
  templateUrl: './projects-list.component.html',
  imports: [
    ButtonModule,
    TooltipModule,
    RouterModule,
    CardModule,
    TableModule,
    ConfirmDialogModule,
  ],
  providers: [ConfirmationService],
})
export class ProjectsListComponent {
  public projects: ProjectDto[] = [];
  public isLoading = true;
  public totalRecords = 0;
  public first = 0;

  constructor(
    private readonly apiService: ApiService,
    private readonly confirmationService: ConfirmationService,
    private readonly toastService: ToastService,
  )
  {
  }

  search(event: Event) {
    this.isLoading = true;
    const searchValue = (event.target as HTMLInputElement).value;

    this.apiService.getProjects({search: searchValue}).subscribe((result) => {
      if (result.isSuccess) {
        this.projects = result.data;
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

    this.apiService.getProjects({orderBy: sortField, page: page, pageSize: rows}).subscribe((result) => {
      if (result.isSuccess) {
        this.projects = result.data;
        this.totalRecords = result.totalItems;
      }

      this.isLoading = false;
    });
  }

  confirmToDelete(project: ProjectDto) {
    this.confirmationService.confirm({
      message: `Are you sure that you want to delete the project '${project.name}'?`,
      accept: () => this.deleteProject(project),
    });
  }

  private deleteProject(project: ProjectDto) {
    this.isLoading = true;

    this.apiService.deleteProject(project.id).subscribe((result) => {
      if (result.isSuccess) {
        this.projects = this.projects.filter((c) => c.id !== project.id);
        this.toastService.showSuccess('A project has been deleted successfully');
      }

      this.isLoading = false;
    });
  }
}
