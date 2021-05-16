using System;

namespace blab1
{
    class Document
    {
        public string Body { set { body.Content = value; } }
        public string Footer { set { footer.Content = value; } }

        Title title;
        Body body;
        Footer footer;
        
        public Document(string title)
        {
            this.title = new Title(title);
            body = new Body();
            footer = new Footer();
        }

        public void Show()
        {
            title.Show();
            body.Show();
            footer.Show();
        }
    }

    class ContentContainer
    {
        public string Content { get; set; }
        public ContentContainer(string content)
        {
            Content = content;
        }
        public virtual void Show()
        {
            Console.WriteLine(Content);
        }
    }

    class Title : ContentContainer
    {
        public Title(string content="Title") : base(content) { }

        public override void Show()
        {
            Console.WriteLine($"===== {Content} =====");
        }
    }

    class Body : ContentContainer
    {
        public Body(string content="Body") : base(content) { }
    }

    class Footer : ContentContainer
    {
        public Footer(string content="Footer") : base(content) { }

        public override void Show()
        {
            Console.WriteLine($"===============");
            base.Show();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Document doc = new Document("Хорошая лабораторная по C#");
            doc.Body = "А похвалите ещё лабораторную";
            doc.Footer = "Ну как её ещё похвалить...";

            doc.Show();
        }
    }
}
