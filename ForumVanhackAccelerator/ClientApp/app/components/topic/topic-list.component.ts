import { Component, Inject, Input, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: "topic-list",
    templateUrl: './topic-list.component.html'
})

export class TopicListComponent implements OnInit {
    topics: Topic[];
    private sub: any;

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        public auth: AuthService
    ) { }

    ngOnInit() {

        this.sub = this.activatedRoute.params.subscribe(params => {

            // In a real app: dispatch action to load the details here.
            var url = this.baseUrl + "api/topic/" + (params.filter || '');

            this.http.get<Topic[]>(url).subscribe(
                result => {
                    this.topics = result;
                    console.log('Called API ' + url + ' and returned ' + JSON.stringify({ data: this.topics }, null, 4));
                },
                error => console.error(error));
        });

        //var filter = +this.activatedRoute.snapshot.params["filter"];
       
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
