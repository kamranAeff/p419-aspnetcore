﻿namespace WebUI.Models.DTOs.Blogs
{
    public class BlogAddDto
    {
        public string Body { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
