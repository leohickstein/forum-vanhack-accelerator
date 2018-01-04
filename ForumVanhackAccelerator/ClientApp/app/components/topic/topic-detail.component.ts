import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { AuthService } from '../../services/auth.service';

@Component({
    selector: "topic",
    templateUrl: './topic-detail.component.html'
})

export class TopicDetailComponent implements OnInit {
    topic: Topic;
    form: FormGroup;

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        private fb: FormBuilder,
        public auth: AuthService,
        @Inject('BASE_URL') private baseUrl: string) {

        // create an empty object from the Topic interface
        this.topic = <Topic>{};
    }

    ngOnInit() {
        var id = +this.activatedRoute.snapshot.params["id"];
        if (id) {
            // fetch the topic from the server
            var url = this.baseUrl + "api/topic/detail/" + id;
            this.http.get<Topic>(url).subscribe(
                result => {
                    this.topic = result;
                    console.log('Called API ' + url + ' and returned ' + JSON.stringify({ data: result }, null, 4));
                    console.log('Called API ' + url + ' and returned ' + JSON.stringify({ data: this.topic }, null, 4));
                },
                error => {
                    console.error(error)
                }
            );
        }
        else {
            console.log("Invalid id: routing back to forum...");
            this.router.navigate(["forum"]);
        }
    }
}
