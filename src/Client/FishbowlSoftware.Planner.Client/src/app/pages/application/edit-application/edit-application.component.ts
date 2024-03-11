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
import {UpdateApplicationCommand} from '@core/models';
import {ApiService, ToastService} from '@core/services';

@Component({
  selector: 'app-edit-application',
  standalone: true,
  templateUrl: './edit-application.component.html',
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
export class EditApplicationComponent implements OnInit {
  public id!: string;
  public isLoading = false;
  public form: FormGroup<UpdateApplicationForm>;

  constructor(
    private readonly apiService: ApiService,
    private readonly toastService: ToastService,
    private readonly route: ActivatedRoute,
  )
  {
    this.form = new FormGroup<UpdateApplicationForm>({
      name: new FormControl('', {validators: Validators.required, nonNullable: true}),
      description: new FormControl(null),
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.id = params['id'];
    });

    this.fetchApplication();
  }

  submit() {
    if (!this.form.valid) {
      return;
    }

    this.isLoading = true;

    const command: UpdateApplicationCommand = {
      id: this.id,
      name: this.form.value.name!,
      description: this.form.value.description!,
    };

    this.apiService.updateApplication(command).subscribe((result) => {
      if (result.isSuccess) {
        this.toastService.showSuccess('The application details has been updated successfully');
        this.form.reset();
      }

      this.isLoading = false;
    });
  }

  private fetchApplication() {
    this.isLoading = true;

    this.apiService.getApplication(this.id).subscribe((result) => {
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

interface UpdateApplicationForm {
  name: FormControl<string>;
  description: FormControl<string | null>;
}
