using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZooManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SqlConnection connection;
        private string connetionString = @"Data Source=localhost,5433;Initial Catalog=ZooManager;User ID=SA;Password=Pass@word;Pooling=False";

        public MainWindow()
        {
            InitializeComponent();
            this.ShowZoos();
            this.ShowAnimals();
        }

        private void listZoos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ShowZooAnimals();
        }

        private void DeleteZoo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connetionString);
                string sqlQuery = "delete from Zoo where id = @zooId";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                connection.Open();

                sqlCommand.Parameters.AddWithValue("@zooId", listZoos.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                connection.Close();
                this.ShowZoos();
            }
        }

        private void AddZoo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //connection = new SqlConnection(connetionString);
                string sqlQuery = "insert into Zoo values (@Location)";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                connection.Open();

                sqlCommand.Parameters.AddWithValue("@Location", myTextBox.Text);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                connection.Close();
                this.ShowZoos();
            }
        }

        private void UpdateZoo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //connection = new SqlConnection(connetionString);
                string sqlQuery = "update Zoo set Location = @Location where Id = @zooId";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                connection.Open();

                sqlCommand.Parameters.AddWithValue("@Location", myTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@zooId", listZoos.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                connection.Close();
                this.ShowZoos();
            }
        }

        private void AddAnimal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //connection = new SqlConnection(connetionString);
                string sqlQuery = "insert into Animal values (@Name)";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                connection.Open();

                sqlCommand.Parameters.AddWithValue("@Name", myTextBox.Text);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                connection.Close();
                this.ShowAnimals();
            }
        }

        private void RemoveAnimal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connetionString);
                string sqlQuery = "delete from Animal where id = @zooId";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                connection.Open();

                sqlCommand.Parameters.AddWithValue("@zooId", listAnimals.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                connection.Close();
                this.ShowAnimals();
            }
        }

        private void UpdateAnimal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //connection = new SqlConnection(connetionString);
                string sqlQuery = "update Animal set Name = @name where Id = @animalId";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                connection.Open();

                sqlCommand.Parameters.AddWithValue("@name", myTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@animalId", listAnimals.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                connection.Close();
                this.ShowAnimals();
            }
        }

        private void AddAnimalToZoo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //connection = new SqlConnection(connetionString);
                string sqlQuery = "insert into ZooAnimal values (@ZooId, @AnimalId)";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                connection.Open();

                sqlCommand.Parameters.AddWithValue("@AnimalId", listAnimals.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@ZooId", listZoos.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                connection.Close();
                this.ShowZooAnimals();
            }
        }

        private void RemoveAnimalFromZoo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // connection = new SqlConnection(connetionString);
                string sqlQuery = "delete from ZooAnimal where AnimalId = @animalId";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                connection.Open();

                sqlCommand.Parameters.AddWithValue("@animalId", listZooAnimals.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                connection.Close();
                this.ShowZooAnimals();
            }
        }

        private void ShowZooAnimals()
        {
            try
            {
                connection = new SqlConnection(connetionString);
                string sqlQuery = "select * from Animal a " +
                    "inner join ZooAnimal za on a.Id = za.AnimalId where za.ZooId = @zooId";

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                using (adapter)
                {
                    sqlCommand.Parameters.AddWithValue("@zooId", listZoos.SelectedValue);
                    DataTable zooAnimalTable = new DataTable();
                    adapter.Fill(zooAnimalTable);
                    listZooAnimals.DisplayMemberPath = "Name";
                    listZooAnimals.SelectedValuePath = "Id";
                    listZooAnimals.ItemsSource = zooAnimalTable.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ShowZoos()
        {
            try
            {
                connection = new SqlConnection(connetionString);
                string sqlCommand = "select * from Zoo";

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand, connection);
                using (adapter)
                {
                    DataTable zooTable = new DataTable();
                    adapter.Fill(zooTable);
                    listZoos.DisplayMemberPath = "Location";
                    listZoos.SelectedValuePath = "Id";
                    listZoos.ItemsSource = zooTable.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ShowAnimals()
        {
            try
            {
                connection = new SqlConnection(connetionString);
                string sqlCommand = "select * from Animal";

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand, connection);
                using (adapter)
                {
                    DataTable animalTable = new DataTable();
                    adapter.Fill(animalTable);
                    listAnimals.DisplayMemberPath = "Name";
                    listAnimals.SelectedValuePath = "Id";
                    listAnimals.ItemsSource = animalTable.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

    }
}
