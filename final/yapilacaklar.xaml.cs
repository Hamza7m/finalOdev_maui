using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using final.Models;
using final.Services;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Controls;
namespace final;

public partial class yapilacaklar : ContentPage
{    private ObservableCollection<Task> tasks;
    private readonly FirebaseClient _firebaseClient ; 

    public yapilacaklar(FirebaseClient firebaseClient)
	{
        BackgroundColor = SettingsService.GetBackgroundColor();
		InitializeComponent();
      
        tasks = new ObservableCollection<Task>();
        TasksListView.ItemsSource = tasks; 
        _firebaseClient = firebaseClient;
        
    }
    private async void OnAddTaskButtonClicked(object sender, EventArgs e)
    {
        string taskName = TaskEntry.Text;
        string kullanciAdi = "user123"; 
        string kullanciSefrisi = "password123";  

        if (!string.IsNullOrWhiteSpace(taskName))
        {
            tasks.Add(new Task { Name = taskName, IsCompleted = false });
        }

        var todoItem = new todo
        {
            Task = taskName,
            kullanciAdi = kullanciAdi,
            kullanciSefrisi = kullanciSefrisi,
            IsCompleted = false
        };

        await _firebaseClient.Child("todo").PostAsync(todoItem);
        TaskEntry.Text = string.Empty;
    }

    private async void OnDeleteTaskButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (button.BindingContext is Task task)
        {
            try
            {
                
                var firebaseTask = (await _firebaseClient
                    .Child("todo")
                    .OnceAsync<todo>())
                    .FirstOrDefault(x => x.Object.Task == task.Name);

                if (firebaseTask != null)
                {
                    await _firebaseClient.Child("todo").Child(firebaseTask.Key).DeleteAsync();
                }

              
                tasks.Remove(task);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while deleting the task: {ex.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Task deletion failed.", "OK");
        }
    }

    private async void OnUpdateTaskButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (button.BindingContext is Task task)
        {
            string updatedTaskName = TaskEntry.Text;
            if (!string.IsNullOrWhiteSpace(updatedTaskName))
            {
                try
                {
                  
                    var firebaseTask = (await _firebaseClient
                        .Child("todo")
                        .OnceAsync<todo>())
                        .FirstOrDefault(x => x.Object.Task == task.Name);

                    if (firebaseTask != null)
                    {
                        await _firebaseClient.Child("todo").Child(firebaseTask.Key).PutAsync(new todo
                        {
                            Task = updatedTaskName
                        });
                    }

                  
                    task.Name = updatedTaskName;
                    TaskEntry.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred while updating the task: {ex.Message}", "OK");
                }
            }
        }
    }
    private async void LoadTasks()
    {
        var todos = await _firebaseClient
            .Child("todo")
            .OnceAsync<todo>();

        tasks.Clear();

        foreach (var todo in todos)
        {
            // Firebase'den gelen veriyi iþliyoruz
            var taskItem = todo.Object;

            tasks.Add(new Task
            {
                Name = taskItem.Task,
                IsCompleted = taskItem.IsCompleted
            });

        
            string kullanciAdi = taskItem.kullanciAdi;
            string kullanciSefrisi = taskItem.kullanciSefrisi;

        }
    }

    private async void OnTaskCheckboxChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = (CheckBox)sender;
        if (checkbox.BindingContext is Task task)
        {
            try
            {
                var firebaseTask = (await _firebaseClient
                    .Child("todo")
                    .OnceAsync<todo>())
                    .FirstOrDefault(x => x.Object.Task == task.Name);

                if (firebaseTask != null)
                {
                    await _firebaseClient.Child("todo").Child(firebaseTask.Key).PutAsync(new todo
                    {
                        Task = task.Name
                    });
                }

                
                task.IsCompleted = e.Value;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while updating the task status: {ex.Message}", "OK");
            }
        }
    }


}
public class Task : INotifyPropertyChanged
{
    private string name;
    private bool isCompleted;

    public string Name
    {
        get => name;
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public bool IsCompleted
    {
        get => isCompleted;
        set
        {
            if (isCompleted != value)
            {
                isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
