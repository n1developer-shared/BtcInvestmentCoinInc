 $(window).scroll(function(){
          $('nav').toggleClass('scrolled',$(this).scrollTop() > 50);
        });

        $(document).ready(function() {
          $("#formButton").click(function() {
            $("#form1").toggle('show');
          });
        })


  //table bgcolor code//

        $(function () {
          $("tr.my-tr").after('<tr class="tr-spacer"/>');
     });



     //Dropdown Code//

     function myFunction() {
      document.getElementById("myDropdown").classList.toggle("show");
    }
    
    // Close the dropdown menu if the user clicks outside of it
    window.onclick = function(event) {
      if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
          var openDropdown = dropdowns[i];
          if (openDropdown.classList.contains('show')) {
            openDropdown.classList.remove('show');
          }
        }
      }
    }



    //DIV hide and show code on same page//

    function show1(){
      
      document.getElementById('div2').style.display="block";
      document.getElementById('div1').style.display="none";
   
     }

     function show2(){
       document.getElementById('div1').style.display="block";
       document.getElementById('div2').style.display="none";
    
      }
