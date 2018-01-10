import { Component, Inject, Input, OnChanges, SimpleChanges } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from "@angular/common/http";
import { AuthService } from '../../services/auth.service';

@Component({
    selector: "post-list",
    templateUrl: './post-list.component.html'
})

export class PostListComponent implements OnChanges {
    @Input() topic: Topic;
    form: FormGroup;
    posts: Post[];

    constructor(
        @Inject('BASE_URL') private baseUrl: string,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        private fb: FormBuilder,
        public auth: AuthService) {

        // create an empty object from the Posts interface
        this.posts = [];

        // initialize the form
        this.createForm();
    }

    ngOnChanges(changes: SimpleChanges) {
        if (typeof changes['topic'] !== "undefined") {

            // retrieve the topic variable change info
            var change = changes['topic'];

            // only perform the task if the value has been changed
            if (!change.isFirstChange()) {
                // execute the Http request and retrieve the result
                this.loadData();
            }
        }
    }

    loadData() {
        var url = this.baseUrl + "api/post/" + this.topic.Id;
        this.http.get<Post[]>(url).subscribe(
        result => {
            this.posts = result;
            console.log('Called API ' + url + ' and returned ' + JSON.stringify({ data: this.posts }, null, 4));
        },
        error => console.error(error));
    }

    createForm() {
        this.form = this.fb.group({
            PostComment: ['', Validators.required]
        });
    }

    resetForm() {
        this.form.setValue({
            PostComment: ''
        });
    }

    onCreate() {
        var url = this.baseUrl + "api/post/";

        // build a temporary post object from form values
        var tempPost = <Post>{};
        tempPost.TopicId = this.topic.Id;
        tempPost.Content = this.form.value.PostComment;
        tempPost.Username = this.auth.getUser()!;

        this.http
            .post<Post>(url, tempPost)
            .subscribe(
                res => {
                    // refresh the post list
                    this.loadData();
                    // reset form
                    this.resetForm();
            },
            error => console.log(error));
    }

    onBack() {
        this.router.navigate(["forum"]);
    }

    onEdit() {
        alert("Not implemented in this version!");
    }

    onDelete(post: Post) {
        if (confirm("Do you really want to delete this post?")) {
            var url = this.baseUrl + "api/post/" + post.Id;
            this.http
                .delete(url)
                .subscribe(res => {
                    // console.log("Post " + post.Id + " has been deleted.");

                    // refresh the post list
                    this.loadData();
                }, error => console.log(error));
        }
    }
}
