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
import {ClientDto, CreateProjectCommand} from '@core/models';
import {ApiService, ToastService} from '@core/services';

@Component({
  selector: 'app-add-project',
  standalone: true,
  templateUrl: './add-project.component.html',
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
export class AddProjectComponent {
  public suggestedClients: ClientDto[] = [];
  public isLoading = false;
  public form: FormGroup<CreateProjectForm>;

  constructor(
    private readonly apiService: ApiService,
    private readonly toastService: ToastService,
  )
  {
    this.form = new FormGroup<CreateProjectForm>({
      name: new FormControl('', {validators: Validators.required, nonNullable: true}),
      client: new FormControl(null, {validators: Validators.required, nonNullable: false}),
      description: new FormControl(null),
    });
  }

  searchClient(event: {query: string}) {
    this.apiService.getClients({search: event.query}).subscribe(result => {
      if (result.isSuccess && result.data) {
        this.suggestedClients = result.data;
      }
    });
  }

  submit() {
    if (!this.form.valid) {
      return;
    }

    this.isLoading = true;

    const command: CreateProjectCommand = {
      name: this.form.value.name!,
      clientId: this.form.value.client!.id,
      description: this.form.value.description!,
    };

    this.apiService.createProject(command).subscribe((result) => {
      if (result.isSuccess) {
        this.toastService.showSuccess('New project has been created successfully');
        this.form.reset();
      }

      this.isLoading = false;
    });
  }
}

interface CreateProjectForm {
  name: FormControl<string>;
  client: FormControl<ClientDto | null>;
  description: FormControl<string | null>;
}
