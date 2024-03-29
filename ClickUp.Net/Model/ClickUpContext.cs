﻿namespace Boson.ClickUp.Net.Model
{
    public class ClickUpContext
    {
        public List<Comment> comments { get; set; }
        public List<List> lists { get; set; }
        public List<Space> spaces { get; set; }
        public List<Team> teams { get; set; }
        public User user { get; set; }
        public Custom_Roles[] custom_roles { get; set; }
    }
}