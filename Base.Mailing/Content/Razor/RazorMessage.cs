namespace Base.Mailing.Content.Razor
{
    using System;
    using System.IO;
    using RazorTemplates.Core;

    public class RazorMessage : IMessageContent
    {
        private string _renderedContent;
        private string _templateName;
        private object _model;
        private ITemplate<dynamic> _stdHeader;
        private ITemplate<dynamic> _stdFooter;

        /// <summary>
        /// Construct for a given template name (will be looked up in the templates folder)
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="model"></param>
        public RazorMessage(string templateName, dynamic model)
        {
            _templateName = templateName;
            _model = model;

            // Load StdHeader and StdFooter templates
            _stdHeader = LoadTemplate(@"Shared\StdHeader");
            _stdFooter = LoadTemplate(@"Shared\StdFooter");
        }

        /// <summary>
        /// Get the content. Will be rendered now if not pre-rendered
        /// </summary>
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

        /// <summary>
        /// Render the razor template. Can be done at any stage, if not done before will be auto
        /// invoked at the point of getting the content
        /// </summary>
        public void Render()
        {
            // If already rendered, return straight away
            if (_renderedContent != null)
            {
                return;
            }

            // Find the template
            ITemplate<dynamic> bodyTemplate = LoadTemplate(_templateName);

            // Run header, footer and body it over the model to generate the rendered content
            _renderedContent =
                _stdHeader.Render(_model)
                + bodyTemplate.Render(_model)
                + _stdFooter.Render(_model);

            // We can now forget the model we don't need it again
            _model = null;
        }

        /// <summary>
        /// Load a template
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private ITemplate<dynamic> LoadTemplate(string templateName)
        {
            // Find the template
            string templateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Templates", string.Format("{0}.cshtml", templateName));
            if (!File.Exists(templateFile))
                throw new FileNotFoundException("Razor template file not found", templateFile);

            // Read the template
            string templateText;
            try
            {
                templateText = File.ReadAllText(templateFile);
            }
            catch
            {
                throw new Exception(string.Format("Error reading razor template file: {0}", templateFile));
            }

            // Compile the template
            var compiler = Template.WithBaseType<TemplateBase>().AddNamespace("System");
            return compiler.Compile(templateText);
        }
    }
}
