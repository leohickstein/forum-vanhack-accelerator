import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormControl, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { AuthService } from '../../services/auth.service';

@Component({
    selector: "topic-edit",
    templateUrl: './topic-edit.component.html'
})

export class TopicEditComponent {
    title: string;
    topic: Topic;
    form: FormGroup;

    // this will be TRUE when editing an existing topic,
    // FALSE when creating a new one.
    editMode: boolean;

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        private fb: FormBuilder,
        public auth: AuthService,
        @Inject("BASE_URL") private baseUrl: string
    ) {
        // create an empty object from the Topic interface
        this.topic = <Topic>{};

        // initialize the form
        this.createForm();

        var id = +this.activatedRoute.snapshot.params["id"];
        if (id) {
            this.editMode = true;

            // fetch the topic from the server
            var url = this.baseUrl + "api/topic/detail/" + id;
            this.http.get<Topic>(url).subscribe(
                result => {
                    this.topic = result;
                    this.title = "Edit - " + this.topic.Title;

                    // update the form with the topic value
                    this.updateForm();
                },
                error => console.error(error)
            );
        } else {
            this.editMode = false;
            this.title = "Create a new Topic";
        }
    }

    createForm() {
        this.form = this.fb.group({
            Title: ["", Validators.required],
            Description: ""
        });
    }

    updateForm() {
        this.form.setValue({
            Title: this.topic.Title,
            Description: this.topic.Description || ""
        });
    }

    onSubmit() {

        // build a temporary topic object from form values
        var tempTopic = <Topic>{};
        tempTopic.Title = this.form.value.Title;
        tempTopic.Description = this.form.value.Description;
        tempTopic.Username = this.auth.getUser()!;

        var url = this.baseUrl + "api/topic";

        if (this.editMode) {
            // don't forget to set the tempTopic Id,
            //   otherwise the EDIT would fail!
            tempTopic.Id = this.topic.Id;

            this.http.put<Topic>(url, tempTopic).subscribe(
                res => {
                    this.topic = res;
                    this.router.navigate(["forum"]);
                },
                error => console.log(error)
            );
        } else {
            this.http.post<Topic>(url, tempTopic).subscribe(
                res => {
                    var v = res;
                    this.router.navigate(["forum"]);
                },
                error => console.log(error)
            );
        }
    }

    onBack() {
        this.router.navigate(["forum"]);
    }

    // retrieve a FormControl
    getFormControl(name: string) {
        return this.form.get(name);
    }

    // returns TRUE if the FormControl is valid
    isValid(name: string) {
        var e = this.getFormControl(name);
        return e && e.valid;
    }

    // returns TRUE if the FormControl has been changed
    isChanged(name: string) {
        var e = this.getFormControl(name);
        return e && (e.dirty || e.touched);
    }

    // returns TRUE if the FormControl is invalid after user changes
    hasError(name: string) {
        var e = this.getFormControl(name);
        return e && (e.dirty || e.touched) && !e.valid;
    }
}
