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
import {CreateApplicationCommand} from '@core/models';
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
  public form: FormGroup<CreateApplicationForm>;

  constructor(
    private readonly apiService: ApiService,
    private readonly toastService: ToastService,
  )
  {
    this.form = new FormGroup<CreateApplicationForm>({
      name: new FormControl('', {validators: Validators.required, nonNullable: true}),
      description: new FormControl(null),
    });
  }

  submit() {
    if (!this.form.valid) {
      return;
    }

    this.isLoading = true;

    const command: CreateApplicationCommand = {
      name: this.form.value.name!,
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
  description: FormControl<string | null>;
}
