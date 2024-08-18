namespace CilindroCrud10195594
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editCilindroId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => Listview.ItemsSource = await _dbService.GetCilindro());
        }

        private async void calcularBtn_Clicked(object sender, EventArgs e)
        {
          
            if (_editCilindroId == 0)
            {
                double dato1, dato2, resultadoArea, areaLateral, areaBase, resultadoVolumen;
              
                dato1 = Convert.ToInt32(radioEntry.Text);
                dato2 = Convert.ToInt32(alturaEntry.Text);

                //Cálculamos el área lateral, fórmula : 2 * pi * radio * altura
                areaLateral = 2 * Math.PI * dato1 * dato2;

                //Cálculamos el área de las dos bases,  fórmula : 2 * pi * radio^2
                areaBase = 2 * Math.PI * Math.Pow(dato1, 2);

                //Cálculamos el área total del cilindro 
                resultadoArea = areaLateral + areaBase;

                //Cálculamos el volumen del cilindro formula : pi * radio^2 * altura
                resultadoVolumen = Math.PI * Math.Pow(dato1, 2) * dato2;

                labelresultadoArea.Text = resultadoArea.ToString();
                labelresultadoVolumen.Text = resultadoVolumen.ToString();

                //Agrega cliente
                await _dbService.Create(new Cilindro
                {
                   Radio = radioEntry.Text,
                   Altura = alturaEntry.Text,
                    ResultadoArea = resultadoArea.ToString(),
                    ResultadoVolumen = resultadoVolumen.ToString()
                });
                Listview.ItemsSource = await _dbService.GetCilindro();
            }
            else
            {
                double dato1, dato2, resultadoArea, resultadoVolumen;

                dato1 = Convert.ToInt32(radioEntry.Text);
                dato2 = Convert.ToInt32(alturaEntry.Text);

                resultadoArea = (3.1415 * (dato1) * 2);
                resultadoVolumen = (3.1415 * dato2 * (dato2) * 2);
                labelresultadoArea.Text = resultadoArea.ToString();
                labelresultadoVolumen.Text = resultadoVolumen.ToString();

                //Edita cliente
                await _dbService.Update(new Cilindro
                {
                    Id = _editCilindroId,
                    Radio = radioEntry.Text,
                    Altura = alturaEntry.Text,
                    ResultadoArea = resultadoArea.ToString(),
                    ResultadoVolumen = resultadoVolumen.ToString()
                });
                _editCilindroId = 0;
            }
            radioEntry.Text = string.Empty;
            alturaEntry.Text = string.Empty ;
            labelresultadoArea.Text = string.Empty;
            labelresultadoVolumen.Text = string.Empty;

            Listview.ItemsSource = await _dbService.GetCilindro();
        }

        private async void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cilindro = (Cilindro)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editCilindroId = cilindro.Id;
                    radioEntry.Text = cilindro.Radio;
                    alturaEntry.Text = cilindro.Altura;
                    labelresultadoArea.Text = cilindro.ResultadoArea;
                    labelresultadoVolumen.Text = cilindro.ResultadoVolumen;
                    break;

                case "Delete":
                    await _dbService.Delete(cilindro);
                    Listview.ItemsSource = await _dbService.GetCilindro();
                    break;
            }
        }


    }

}

