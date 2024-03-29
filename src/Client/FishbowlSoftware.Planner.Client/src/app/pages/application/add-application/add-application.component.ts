import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {InputTextModule} from 'primeng/inputtext';
import {InputTextareaModule} from 'primeng/inputtextarea';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {ValidationSummaryComponent} from '@shared/components';
import {CreateApplicationCommand, ProjectDto} from '@core/models';
import {ApiService, ToastService} from '@core/services';

@Component({
  selector: 'app-add-application',
  standalone: true,
  templateUrl: './add-application.component.html',
  imports: [
    RouterModule,
    ButtonModule,
    CardModule,
    ProgressSpinnerModule,
    ValidationSummaryComponent,
    AutoCompleteModule,
    ReactiveFormsModule,
    InputTextModule,
    InputTextareaModule,
  ],
})
export class AddApplicationComponent {
  public isLoading = false;
  public suggestedProjects: ProjectDto[] = [];
  public form: FormGroup<CreateApplicationForm>;

  constructor(
    private readonly apiService: ApiService,
    private readonly toastService: ToastService,
  )
  {
    this.form = new FormGroup<CreateApplicationForm>({
      name: new FormControl('', {validators: Validators.required, nonNullable: true}),
      project: new FormControl(null, {validators: Validators.required, nonNullable: false}),
      description: new FormControl(null),
    });
  }

  searchProject(event: {query: string}) {
    this.apiService.getProjects({search: event.query}).subscribe(result => {
      if (result.isSuccess && result.data) {
        this.suggestedProjects = result.data;
      }
    });
  }

  submit() {
    if (!this.form.valid) {
      return;
    }

    this.isLoading = true;

    const command: CreateApplicationCommand = {
      name: this.form.value.name!,
      projectId: this.form.value.project!.id,
      description: this.form.value.description!,
    };

    this.apiService.createApplication(command).subscribe((result) => {
      if (result.isSuccess) {
        this.toastService.showSuccess('New application has been created successfully');
        this.form.reset();
      }

      this.isLoading = false;
    });
  }
}

interface CreateApplicationForm {
  name: FormControl<string>;
  project: FormControl<ProjectDto | null>;
  description: FormControl<string | null>;
}
