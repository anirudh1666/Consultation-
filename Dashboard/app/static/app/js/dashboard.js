/**This function is responsible for calculating the % change in data from last week
 * and this week. It then displays the % change (to nearest integer) on index.html above
 * the d3.js graphs. The color is red if % decrease, green if increase and yellow if no change.
 * @param {*} id is the id of the document element that displays % change.
 * @param {*} data is the data that the django database returns for the d3.js graph.
 */
function change_in_vals(id, data) {

   const text = document.getElementById(id);
   var this_week = data[data.length - 1].value;
   var last_week = data[data.length - 2].value;
   var p_change = Math.round(((this_week - last_week) / last_week) * 100);
   if (p_change < 0) {
      text.innerHTML = (-1 * p_change) + "% Decrease";
      text.style.color = "red";
   }
   else if (p_change > 0) {
      text.innerHTML = p_change + "% Increase";
      text.style.color = "green";
   }
   else {
      text.innerHTML = "No change";
      text.style.color = "yellow";
   }
}

/**Executes an ajax request to django server to recover data. The request
 * varies depending on url but it is dependent on location. Once we recover the data
 * we draw the d3.js graphs and calculate % change from last week and total value for this week.
 * We update index.html accordingly.
 * @param {*} url url tells us what kind of data we are looking for. You can see urls in urls.py
 * @param {*} location sorts the data by region. We get data relevant for location.
 * @param {*} id is the id of the div that will contain the svg.
 * @param {*} header_id the id of the text above the d3 graphs. Specifically the element that shows the number for each week.
 * @param {*} color color we want the line in the graph to be.
 */
function execute_ajax(url, location, id, header_id, color) {

   $.ajax({
      url: url,
      type: 'GET',
      data: {
         'location' : location
      },
      dataType: 'json',
      success: function(json) {
         var data = [];
         for (i = 0; i != json.length; ++i) {
            var tmp = {value: json[i].value, time : new Date(json[i].year, json[i].month, json[i].day)};
            data.push(tmp);
         }
         update_svg(data, id, color);
         document.getElementById(header_id).innerHTML = data[data.length - 1].value;
         change_in_vals(header_id + "Text", data);
      }
   });
}

/**Called when user selects a region on map. We first delete the existing svg elements then move onto 
 * getting data for each graph and drawing them.
 * @param {*} location is the location that the user selects on the graph.
 */
function render_graphs(location) {

   execute_ajax('get/ajax/get/requests', location, "#totalRequests", "totalRequestsChange", "red");
   execute_ajax('get/ajax/get/new/users', location, "#newUsers", "newUsersChange", "lawngreen");
   execute_ajax("get/ajax/get/uniq/requests", location, "#uniqRequests", "uniqRequestsChange", "aqua");
}

/** Used to update svg. If you use draw_svg it draws a completely new svg underneath it. This one updates
 * the existing svg to display the new data.
 * @param {*} data the new dataset we want to display.
 * @param {*} id the id of the div where we are drawing the svg.
 * @param {*} color color of the line.
 */
function update_svg(data, id, color) {

   var margin = {top: 10, right: 30, bottom: 30, left: 60},
      width = 300 - margin.left - margin.right,
      height = 280 - margin.top - margin.bottom;

   var svg = d3.select(id);
   svg.select('.line-graph').remove();
   svg = svg.select('.svg-content-responsive')
            .append("g")
            .classed('line-graph', true)
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

   var x = d3.scaleTime()
             .domain(d3.extent(data, function(d) { return d.time; }))
             .range([0, width]);
 
   var y = d3.scaleLinear()
             .domain([d3.min(data, function(d) { return d.value; }) - 10,
               d3.max(data, function(d) { return +d.value; }) + 10])
             .range([height, 0]);
   svg.append("g")
      .attr("stroke", "gray")
      .call(d3.axisLeft(y));
 
   svg.append("path")
      .datum(data)
      .attr("fill", "none")
      .attr("stroke", color)
      .attr("stroke-width", 1.5)
      .attr("d", d3.line()
              .x(function(d) { return x(d.time)})
              .y(function(d) { return y(d.value)})
            );
}

/**Does all the drawing of graphs. the data must be in a specific format for this to work. It must be
 * an array of objects containing a value field and time field which is a Date object. E.g. {value: 12043, time: new Date(2021,3,10)}
 * The svg drawn is also responsive so it can deal with resizing and such.
 * @param {*} data array of objects in the format specified above. the django sends data in the correct format so it isnt too hard to manipulate
 *                 it into the desired form.
 * @param {*} id the id of the div that the svg will be drawn in.
 * @param {*} color color of the lines.
 */
function draw_svg(data, id, color) {

   var margin = {top: 10, right: 30, bottom: 30, left: 60},
      width = 300 - margin.left - margin.right,
      height = 280 - margin.top - margin.bottom;
   
   var svg = d3.select(id)
               .append("div")
               .classed("svg-container", true)
               .append("svg")
               .attr("preserveAspectRatio", "xMinYMin meet")
               .attr("viewBox", "0 0 300 400")
               .classed("svg-content-responsive", true)
               .append("g")
               .classed('line-graph', true)
               .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
 
   var x = d3.scaleTime()
             .domain(d3.extent(data, function(d) { return d.time; }))
             .range([0, width]);
 
   var y = d3.scaleLinear()
             .domain([d3.min(data, function(d) { return d.value; }) - 10,
               d3.max(data, function(d) { return +d.value; }) + 10])
             .range([height, 0]);
   svg.append("g")
      .attr("stroke", "gray")
      .call(d3.axisLeft(y));
 
   svg.append("path")
      .datum(data)
      .attr("fill", "none")
      .attr("stroke", color)
      .attr("stroke-width", 1.5)
      .attr("d", d3.line()
              .x(function(d) { return x(d.time)})
              .y(function(d) { return y(d.value)})
            );
}

/**Recovers data for all of UK rather than being regional. This is called when the document is loaded. The main
 * difference between here and render_graphs is that the ajax get request doesnt have any data in it.
 * @param {*} url url tells us which data we want. you can view urls in urls.py.
 * @param {*} id id of the div that the svgs will be drawn in.
 * @param {*} header_id the id of the piece of text that displays the count for the current week. 
 * @param {*} color color of lines in linegraph.
 */
function all_of_UK(url, id, header_id, color, drawn) {

   $.ajax({
      url: url,
      type: 'GET',
      success: function(json) {
         var data = [];
         for (i = 0; i != json.length; ++i) {
            var tmp = {value: json[i].value, time : new Date(json[i].year, json[i].month, json[i].day)};
            data.push(tmp);
         }
         if (drawn) {
            update_svg(data, id, color);
         }
         else {
            draw_svg(data, id, color);
         }
         document.getElementById(header_id).innerHTML = data[data.length - 1].value;
         change_in_vals(header_id + "Text", data);
      }
   });
}

function display_all_of_uk(drawn) {

   all_of_UK('get/ajax/all/new/users', '#newUsers', 'newUsersChange', 'lawngreen', drawn);
   all_of_UK('get/ajax/all/requests', '#totalRequests', 'totalRequestsChange', 'red', drawn);
   all_of_UK('get/ajax/all/uniq/requests', '#uniqRequests', 'uniqRequestsChange','aqua', drawn);
}

//Just handles calling all_of_UK for all the required data.
window.onload = display_all_of_uk(false)
