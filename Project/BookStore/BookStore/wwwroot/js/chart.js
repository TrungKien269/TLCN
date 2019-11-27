let myChart = document.getElementById('myChart').getContext('2d');


let massPopChart = new Chart(myChart, {
    type: 'bar',
    data: {
        labels: ['January', 'Feburary', 'March', 'April', 'May', 'June','July','August','September','October','November','December'],
        datasets: [{
            label: 'Book sale',
            data: [
                500,
                300,
                400,
                200,
                600,
                700,
                720,
                670,
                654,
                582,
                990,
                762
            ],
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)',            
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'  
                
                
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)',            
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'  
            ],
            borderWidth: 1
        }]

    },
    options: {
        title: {
            display: true,
            text: 'Book Sold by Month',
            fontSize: 25,            
        },
        legend:{
            display: false,
            position:'right',
            labels:{
                fontColor: '#000'
            }
        },
        layout:{
            padding:{
                left:50,
                right:0,
                top:0,
                bottom:0
            }
        },
        tooltips:{
            enabled: true
        },
        
        scales:{
            yAxes:[{
                ticks:{
                    beginAtZero: true
                }
            }]
        }

    },

});

let myChart2 = document.getElementById('myChart2').getContext('2d');
let massPopChart2 = new Chart(myChart2, {
    type: 'line',
    data: {
        labels: ['January', 'Feburary', 'March', 'April', 'May', 'June','July','August','September','October','November','December'],
        datasets: [{
            label: 'Book sale',
            data: [
                500,
                300,
                400,
                200,
                600,
                700,
                720,
                670,
                654,
                582,
                990,
                762
            ],
            backgroundColor: [
               'transparent'
                
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)',            
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'  
            ],
            borderWidth: 1
        }]

    },
    options: {
        title: {
            display: true,
            text: 'Book Sold by Month',
            fontSize: 25,            
        },
        legend:{
            display: false,
            position:'right',
            labels:{
                fontColor: '#000'
            }
        },
        layout:{
            padding:{
                left:50,
                right:0,
                top:0,
                bottom:0
            }
        },
        tooltips:{
            enabled: true
        },
        
        scales:{
            yAxes:[{
                ticks:{
                    beginAtZero: true
                }
            }]
        }

    },

});