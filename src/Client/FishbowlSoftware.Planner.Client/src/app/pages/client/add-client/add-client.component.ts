import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {ValidationSummaryComponent} from '@shared/components';
import {CreateClientCommand, UserDto} from '@core/models';
import {ApiService, ToastService} from '@core/services';

@Component({
  selector: 'app-add-client',
  standalone: true,
  templateUrl: './add-client.component.html',
  imports: [
    RouterModule,
    ButtonModule,
    CardModule,
    ProgressSpinnerModule,
    ValidationSummaryComponent,
    AutoCompleteModule,
    ReactiveFormsModule,
  ],
})
export class AddClientComponent {
  public isLoading = false;
  public suggestedUsers: UserDto[] = [];
  public form: FormGroup<CreateClientForm>;

  constructor(
    private readonly apiService: ApiService,
    private readonly toastService: ToastService,
  )
  {
    this.form = new FormGroup<CreateClientForm>({
      name: new FormControl('', {validators: Validators.required, nonNullable: true}),
      userAccount: new FormControl(null),
    });
  }

  searchUser(event: {query: string}) {
    this.apiService.getUsers({search: event.query}).subscribe(result => {
      if (result.isSuccess && result.data) {
        this.suggestedUsers = result.data;
      }
    });
  }

  submit() {
    if (!this.form.valid) {
      return;
    }

    this.isLoading = true;

    const command: CreateClientCommand = {
      name: this.form.value.name!,
      userId: this.form.value.userAccount?.id,
    };

    this.apiService.createClient(command).subscribe((result) => {
      if (result.isSuccess) {
        this.toastService.showSuccess('New client has been created successfully');
        this.form.reset();
      }

      this.isLoading = false;
    });
  }
}

interface CreateClientForm {
  name: FormControl<string>;
  userAccount: FormControl<UserDto | null>;
}
