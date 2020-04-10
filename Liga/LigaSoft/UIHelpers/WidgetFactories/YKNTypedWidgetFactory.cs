using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using LigaSoft.Models.Enums;

namespace LigaSoft.UIHelpers.WidgetFactories
{
    public class YKNTypedWidgetFactory<TViewModel> : ITypedWidgetFactory<TViewModel>
    {
        private readonly HtmlHelper<TViewModel> _helper;

        public YKNTypedWidgetFactory(HtmlHelper<TViewModel> helper)
        {
            _helper = helper;
        }

	    public DisplayFor<TViewModel, TProperty> DisplayFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
		    return new DisplayFor<TViewModel, TProperty>(_helper, ex);
	    }

	    public CheckBoxFor<TViewModel, bool> CheckBoxFor(Expression<Func<TViewModel, bool>> ex)
	    {
			return new CheckBoxFor<TViewModel, bool>(_helper, ex);
		}

	    public DisplayMultilineFor<TViewModel, TProperty> DisplayMultilineFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
			return new DisplayMultilineFor<TViewModel, TProperty>(_helper, ex);
		}

	    public EditorFor<TViewModel, TProperty> EditorFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
			return new EditorFor<TViewModel, TProperty>(_helper, ex);
		}

	    public TextAreaFor<TViewModel, TProperty> TextAreaFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
		    return new TextAreaFor<TViewModel, TProperty>(_helper, ex);
	    }

		public ComboFor<TViewModel, TProperty> ComboFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
		    return new ComboFor<TViewModel, TProperty>(_helper, ex);
	    }

	    public Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TViewModel, TProperty> Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
			return new Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TViewModel, TProperty>(_helper, ex);
		}

	    public AutocompleteFor<TViewModel, TProperty> AutocompleteFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
		    return new AutocompleteFor<TViewModel, TProperty>(_helper, ex);
	    }

		public SiNoFor<TViewModel, TProperty> SiNoFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
		    return new SiNoFor<TViewModel, TProperty>(_helper, ex);
		}

	    public WebCamFor<TViewModel, TProperty> WebCamFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
		    return new WebCamFor<TViewModel, TProperty>(_helper, ex);
	    }

	    public DatePickerFor<TViewModel, TProperty> DatePickerFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
			return new DatePickerFor<TViewModel, TProperty>(_helper, ex);
		}

	    public BrowseFileFor<TViewModel, TProperty> BrowseFileFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
		    return new BrowseFileFor<TViewModel, TProperty>(_helper, ex);
	    }

		public Button Button(string id)
	    {
		    return new Button(_helper, id);
	    }

		public DropdownButton DropdownButton()
	    {
		    return new DropdownButton(_helper);
	    }

	    public AutoComplete<TViewModel> Autocomplete(string id)
	    {
		    return new AutoComplete<TViewModel>(_helper, id);
	    }

	    public UploadFileFor<TViewModel, TProperty> UploadFile<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
			return new UploadFileFor<TViewModel, TProperty>(_helper, ex);
		}

	    public Form Form(FormSizeEnum maxWidth = FormSizeEnum.Small)
	    {
		    return new Form(_helper, maxWidth);
	    }

	    public Form Form(string method, string controller, FormSizeEnum maxWidth = FormSizeEnum.Small)
	    {
		    return new Form(_helper, method, controller, maxWidth);
	    }

		public GridBuilder<TViewModel> Grid()
	    {
		    return new GridBuilder<TViewModel>(_helper);
	    }

	    public BuscadorFor<TViewModel, TProperty> Buscador<TProperty>(Expression<Func<TViewModel, TProperty>> ex)
	    {
		    return new BuscadorFor<TViewModel, TProperty>(_helper, ex);
	    }

	    public void FooterGuardarCancelar(string guardarLabel = "Guardar", string cancelarLabel = "Cancelar")
	    {
		    var guardar = BotonGuardar(guardarLabel).ToHtmlString();
		    var cancelar = BotonCancelar(cancelarLabel).ToHtmlString();

		    _helper.ViewContext.Writer.Write($@"<div class='row form-footer'>
													<div class='col-sm-offset-6 col-sm-3'>{cancelar}</div>
													<div class='col-sm-3'>{guardar}</div>
												</div>");
	    }

	    public void FooterVolver(string label = "Volver")
	    {
		    var volver = new Button(_helper, label).FullWidth().Color(BootstrapColorEnum.Danger).OnClickVolverAlIndice().ToHtmlString();

		    _helper.ViewContext.Writer.Write($@"<div class='row form-footer'>
													<div class='col-sm-offset-9 col-sm-3'>{volver}</div>													
												</div>");
	    }

	    public Button BotonCancelar(string label = "Cancelar")
	    {
		    return new Button(_helper, label).FullWidth().Color(BootstrapColorEnum.Danger).OnClickVolverAlIndice();
	    }

	    public Button BotonGuardar(string label = "Guardar")
	    {
		    return new Button(_helper, label).Color(BootstrapColorEnum.Success).Submit().FullWidth();
	    }
	}
}