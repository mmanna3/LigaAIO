using System;
using System.Linq.Expressions;
using LigaSoft.Models.Enums;

namespace LigaSoft.UIHelpers.WidgetFactories
{
    public interface ITypedWidgetFactory<TViewModel>
    {		
	    DisplayFor<TViewModel, TProperty> DisplayFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
	    CheckBoxFor<TViewModel, bool> CheckBoxFor(Expression<Func<TViewModel, bool>> ex);
		DisplayMultilineFor<TViewModel, TProperty> DisplayMultilineFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
		EditorFor<TViewModel, TProperty> EditorFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
	    TextAreaFor<TViewModel, TProperty> TextAreaFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
		ComboFor<TViewModel, TProperty> ComboFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
	    Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TViewModel, TProperty> Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
	    AutocompleteFor<TViewModel, TProperty> AutocompleteFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
		SiNoFor<TViewModel, TProperty> SiNoFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
	    WebCamFor<TViewModel, TProperty> WebCamFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
	    DatePickerFor<TViewModel, TProperty> DatePickerFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
	    BrowseFileFor<TViewModel, TProperty> BrowseFileFor<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
	    AutoComplete<TViewModel> Autocomplete(string id);
		UploadFileFor<TViewModel, TProperty> UploadFile<TProperty>(Expression<Func<TViewModel, TProperty>> ex);
		Form Form(FormSizeEnum maxWidth = FormSizeEnum.Small);
	    Form Form(string method, string controller, FormSizeEnum maxWidth = FormSizeEnum.Small);
		Button Button(string id);
	    DropdownButton DropdownButton();
		void FooterGuardarCancelar(string guardarLabel = "Guardar", string cancelarLabel = "Cancelar");
	    void FooterVolver(string label = "Volver");
	    Button BotonCancelar(string label = "Cancelar");
	    Button BotonGuardar(string label = "Guardar");	    
    }
}