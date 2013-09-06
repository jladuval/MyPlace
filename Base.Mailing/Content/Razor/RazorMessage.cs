namespace Base.Mailing.Content.Razor
{
    using System;
    using System.IO;
    using RazorTemplates.Core;

    public class RazorMessage : IMessageContent
    {
        private readonly string _templateName;

        private readonly ITemplate<dynamic> _stdHeader;

        private readonly ITemplate<dynamic> _stdFooter;
        
        private object _model;
        
        private string _renderedContent;

        public string Content
        {
            get
            {
                if (_renderedContent == null)
                {
                    Render();
                }
                return _renderedContent;
            }
        }

        public RazorMessage(string templateName, dynamic model)
        {
            _templateName = templateName;
            _model = model;

            _stdHeader = LoadTemplate(@"Shared\StdHeader");
            _stdFooter = LoadTemplate(@"Shared\StdFooter");
        }

        public void Render()
        {
            if (_renderedContent != null)
            {
                return;
            }

            var bodyTemplate = LoadTemplate(_templateName);

            _renderedContent =
                _stdHeader.Render(_model)
                + bodyTemplate.Render(_model)
                + _stdFooter.Render(_model);

            _model = null;
        }

        private ITemplate<dynamic> LoadTemplate(string templateName)
        {
            var templateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Templates", string.Format("{0}.cshtml", templateName));
            if (!File.Exists(templateFile))
                throw new FileNotFoundException("Razor template file not found", templateFile);

            string templateText;

            try
            {
                templateText = File.ReadAllText(templateFile);
            }
            catch
            {
                throw new Exception(string.Format("Error reading razor template file: {0}", templateFile));
            }

            var compiler = Template.WithBaseType<TemplateBase>().AddNamespace("System");

            return compiler.Compile(templateText);
        }
    }
}
