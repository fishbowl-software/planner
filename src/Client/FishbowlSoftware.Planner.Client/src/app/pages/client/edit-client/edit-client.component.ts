import {Component, OnInit} from '@angular/core';
import {ReactiveFormsModule, FormGroup, FormControl, Validators} from '@angular/forms';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {UserDto, UpdateClientCommand} from '@core/models';
import {ApiService, ToastService} from '@core/services';
import {ValidationSummaryComponent} from '@shared/components';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {ProgressSpinnerModule} from 'primeng/progressspinner';

@Component({
  selector: 'app-edit-client',
  standalone: true,
  templateUrl: './edit-client.component.html',
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
export class EditClientComponent implements OnInit {
  public id!: string;
  public isLoading = false;
  public suggestedUsers: UserDto[] = [];
  public form: FormGroup<UpdateClientForm>;

  constructor(
    private readonly apiService: ApiService,
    private readonly toastService: ToastService,
    private readonly route: ActivatedRoute,
  )
  {
    this.form = new FormGroup<UpdateClientForm>({
      name: new FormControl('', {validators: Validators.required, nonNullable: true}),
      userAccount: new FormControl(null),
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.id = params['id'];
    });

    this.fetchClient();
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

    const command: UpdateClientCommand = {
      id: this.id,
      name: this.form.value.name!,
      userId: this.form.value.userAccount?.id,
    };

    console.log(command);
    

    this.apiService.updateClient(command).subscribe((result) => {
      if (result.isSuccess) {
        this.toastService.showSuccess('The client details has been updated successfully');
        this.form.reset();
      }

      this.isLoading = false;
    });
  }

  private fetchClient() {
    this.isLoading = true;

    this.apiService.getClient(this.id).subscribe((result) => {
      if (result.isSuccess) {
        this.form.patchValue({
          name: result.data.name,
          userAccount: result.data.user,
        });
      }

      this.isLoading = false;
    })
  }
}

interface UpdateClientForm {
  name: FormControl<string>;
  userAccount: FormControl<UserDto | null>;
}
