import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {InputTextModule} from 'primeng/inputtext';
import {InputTextareaModule} from 'primeng/inputtextarea';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {ValidationSummaryComponent} from '@shared/components';
import {ClientDto, UpdateProjectCommand} from '@core/models';
import {ApiService, ToastService} from '@core/services';

@Component({
  selector: 'app-edit-project',
  standalone: true,
  templateUrl: './edit-project.component.html',
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
export class EditProjectComponent implements OnInit {
  public id!: string;
  public isLoading = false;
  public suggestedClients: ClientDto[] = [];
  public form: FormGroup<UpdateProjectForm>;

  constructor(
    private readonly apiService: ApiService,
    private readonly toastService: ToastService,
    private readonly route: ActivatedRoute,
  )
  {
    this.form = new FormGroup<UpdateProjectForm>({
      name: new FormControl('', {validators: Validators.required, nonNullable: true}),
      client: new FormControl(null, {validators: Validators.required, nonNullable: false}),
      description: new FormControl(null),
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.id = params['id'];
    });

    this.fetchProject();
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

    const command: UpdateProjectCommand = {
      id: this.id,
      name: this.form.value.name!,
      description: this.form.value.description!,
    };

    this.apiService.updateProject(command).subscribe((result) => {
      if (result.isSuccess) {
        this.toastService.showSuccess('The project details has been updated successfully');
        this.form.reset();
      }

      this.isLoading = false;
    });
  }

  private fetchProject() {
    this.isLoading = true;

    this.apiService.getProject(this.id).subscribe((result) => {
      if (result.isSuccess) {
        this.form.patchValue({
          name: result.data.name,
          description: result.data.description,
        });
      }

      this.isLoading = false;
    })
  }
}

interface UpdateProjectForm {
  name: FormControl<string>;
  client: FormControl<ClientDto | null>;
  description: FormControl<string | null>;
}
