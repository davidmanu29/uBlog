import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { BlogPost } from '../../blog-post/models/blog-post.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrl: './blog-details.component.css'
})
export class BlogDetailsComponent implements OnInit{
  
  url: string | null = null;
  blogPost$? : Observable<BlogPost>;

  constructor(private route: ActivatedRoute,
              private blogPostService: BlogPostService) {

  }


  ngOnInit(): void {
    this.route.paramMap
    .subscribe({
      next: (params) => {
        this.url = params.get('url');
      }
    })

    if (this.url){
      this.blogPost$ = this.blogPostService.getBlogPostByUrlHandle(this.url);
    }
  }
}
