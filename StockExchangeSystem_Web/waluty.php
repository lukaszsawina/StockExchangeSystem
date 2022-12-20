<?php
include_once 'parts/header.php';
?>

            <!-- Description Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <i class="fa-solid fa-dollar-sign fa-3x text-primary"></i>
                            <div class="ms-3">
                                <h1 class="text-body mb-2">Kursy walut</p>
                                <h6 class="mb-0">Dolar to najważniejsza na świecie waluta transakcyjna i rezerwowa, dla której aktualnie nie ma żadnej realnej alternatywy.</h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Description End -->

            <!-- Table Start-->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-12">
                        <div class="bg-secondary rounded h-100 p-4">
                            <h6 class="mb-4">Najpopularniejsze kryptowaluty</h6>
                            <div class="table-responsive">
                                <table id="example" class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col"> </th>
                                            <th scope="col">#</th>
                                            <th scope="col">Nazwa</th>
                                            <th scope="col">Symbol</th>
                                            <th scope="col">Kurs w USD</th>
                                            <th scope="col">Kurs w PLN</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="USD" class="waluty">
                                            <td><input id="USDchk" class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">1</th>
                                            <td>dolar amerykański</td>
                                            <td>USD</td>
                                            <td>222</td>
                                            <td>400,47B $</td>
                                        </tr>
                                        <tr id="PLN" class="waluty">
                                            <td><input id="PLNchk" class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">2</th>

                                            <td>polski złoty</td>
                                            <td>PLN</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="AUD" class="waluty">
                                            <td><input id="AUDchk" class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">3</th>

                                            <td>dolar australijski</td>
                                            <td>AUD</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="CAD" class="waluty">
                                            <td><input id="CADCchk" class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">4</th>

                                            <td>dolar kanadyjski</td>
                                            <td>CAD</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="EURO" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">5</th>

                                            <td>euro</td>
                                            <td>EUR</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="CHF" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">6</th>

                                            <td>frank szwajcarski</td>
                                            <td>CHF</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="JPY" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">7</th>

                                            <td>jen japoński</td>
                                            <td>JPY</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="CZK" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">8</th>

                                            <td>korona czeska</td>
                                            <td>CZK</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="MXN" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">9</th>

                                            <td>peso meksykańskie</td>
                                            <td>MXN</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="DKK" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">10</th>

                                            <td>korona duńska</td>
                                            <td>DKK</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="INR" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">11</th>

                                            <td>rupia indyjska</td>
                                            <td>INR</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="NOK" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">12</th>

                                            <td>korona norweska</td>
                                            <td>NOK</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>


                                        </tr>
                                        <tr id="ILS" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">13</th>

                                            <td>nowy izraelski szekel</td>
                                            <td>ILS</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="SEK" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">14</th>

                                            <td>korona szwedzka</td>
                                            <td>SEK</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>
                                        <tr id="PHP" class="waluty">
                                            <td><input class="kryptocheck" type="checkbox"></td>
                                            <th scope="row">15</th>

                                            <td>peso filipińskie</td>
                                            <td>PHP</td>
                                            <td>20.859,4</td>
                                            <td>400,47B $</td>

                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Table End-->

            <!-- Calculator Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <i class="fa-solid fa-calculator fa-3x text-primary"></i>
                            <div class="ms-3">
                                <h1 class="text-body mb-2">
                                  Kalkulator Walut
                                <h6 class="mb-0"><p>Ilosc: <input type="text" id="firstNumber" /><br></p> Przelicz z <select name="selection" id="select_1">
                                <option value="USD">USD</option>
                                <option value="PLN">PLN</option>
                                <option value="EUR">EUR</option>
                                <option value="YEN">YEN</option>
                                <option value="CZK">CZK</option>
                              </select>
                              na
                              <select name="selection" id="select_2">
                                <option value="USD">USD</option>
                                <option value="PLN">PLN</option>
                                <option value="EUR">EUR</option>
                                <option value="YEN">YEN</option>
                                <option value="CZK">CZK</option>
                              </select>
                              <form>
                            <!-- Waluta_PLN : <input type="text" id="firstNumber" /><br>
                            Waluta_USD: <input type="text" id="secondNumber" /><br> -->
                            <div><br><p>
                            <input type="button" onClick="to_any()" Value="Przelicz" /></p>
                            <!-- <input type="button" onClick="to_PLN()" Value="to_PLN" /> -->
                            </div>
                            </form>
                            <!-- <p>result : <br>
                            <span id = "result"></span>
                            </p> -->
                            <p>Rezultat:
                            <span id = "result_2"></span>
                            </p>
                            </h6>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <!-- Calculator End -->

            <script>
document.getElementById("navwaluty").classList.add('active');
  //Calculator Script Start
  //   function multiplyNumbers(x,y){
  //       return x*y;
  //   }
  //   var z= multiplyNumbers(4,5); // Function is called, return value will end up in z
  var result = 0;
function to_USD()
{
    var x = document.getElementById("select_1").value;
    num1 = document.getElementById("firstNumber").value;
    if(x == "PLN"){
        num2 = 0.2184;
        result = num1 * num2;
      }
    if(x == "EUR"){
      num2 = 1.0279;
      result = num1 * num2;
    }
    if(x == "USD"){
      num2 = 1.0;
      result = num1 * num2;
    }
    if(x == "CZK"){
      num2 = 0.0422;
      result = num1 * num2;
    }
    if(x == "YEN"){
      num2 = 0.0071 ;
      result = num1 * num2;
    }

}
function to_any()
{
  to_USD();
  var y = document.getElementById("select_2").value;
  if(y == "PLN"){
    num1 = result;
    num2 = 4.5776;
  }
  if(y == "CZK"){
    // num1 = document.getElementById("result").innerHTML;
    num1 = result;
    num2 = 23.6900;
  }
  if(y == "EUR"){
    // num1 = document.getElementById("result").innerHTML;
    num1 = result;
    num2 = 0.9729;
  }
  if(y == "YEN"){
    // num1 = document.getElementById("result").innerHTML;
    num1 = result;
    num2 = 141.32950;
  }
  if(y == "USD"){
    // num1 = document.getElementById("result").innerHTML;
    num1 = result;
    num2 = 1.00;
  }
  document.getElementById("result_2").innerHTML = (num1 * num2).toFixed(2) + " " +y;

}//calculator script end
    </script>

<?php
include_once 'parts/footer.php';
?>