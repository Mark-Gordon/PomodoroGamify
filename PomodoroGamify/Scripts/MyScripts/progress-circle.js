var wrapper = document.getElementById('progress');
var start = 0;


var colours = {
    fill: '#337ab7',
    track: '#555555',
    text: '#337ab7',
    stroke: '#FFFFFF',
}

var radius = 150;
var border = 15;
var strokeSpacing = 4;
var endAngle = Math.PI * 2;
var formatText = d3.format('.0%');
var boxSize = radius * 2;
var progress = start;
var step = percentage < start ? -0.01 : 0.01;

//Define the circle
var circle = d3.arc()
    .startAngle(0)
    .innerRadius(radius)
    .outerRadius(radius - border);

//setup SVG wrapper
var svg = d3.select(wrapper)
    .append('svg')
    .attr('width', boxSize)
    .attr('height', boxSize);

// ADD Group container
var g = svg.append('g')
    .attr('transform', 'translate(' + boxSize / 2 + ',' + boxSize / 2 + ')');

//Setup track
var track = g.append('g').attr('class', 'radial-progress');
track.append('path')
    .attr('class', 'radial-progress__background')
    .attr('fill', colours.track)
    .attr('stroke', colours.stroke)
    .attr('stroke-width', strokeSpacing + 'px')
    .attr('d', circle.endAngle(endAngle));

//Add colour fill
var value = track.append('path')
    .attr('class', 'radial-progress__value')
    .attr('fill', colours.fill)
    .attr('stroke', colours.stroke)
    .attr('stroke-width', strokeSpacing + 'px');

//Add text value
var numberText = track.append('text')
    .style("font-size", "20px")
    .style("font-weight", "bold")
    .attr('class', 'radial-progress__text')
    .attr('fill', colours.text)
    .attr('text-anchor', 'middle')
    .attr('dy', '-50');
var numberText2 = track.append('text')
    .style("font-size", "20px")
    .style("font-weight", "bold")
    .attr('class', 'radial-progress__text')
    .attr('fill', colours.text)
    .attr('text-anchor', 'middle')
    .attr('dy', '0');
var numberText3 = track.append('text')
    .style("font-size", "20px")
    .style("font-weight", "bold")
    .attr('class', 'radial-progress__text')
    .attr('fill', colours.text)
    .attr('text-anchor', 'middle')
    .attr('dy', '50');




function update(progress) {
    //update position of endAngle
    value.attr('d', circle.endAngle(endAngle * progress));
    //update text value
    numberText.text("Level : " + level);
    numberText2.text("% To level : " + formatText(progress));
    numberText3.text("XP To Level : " + experienceToLevelUp);

}

function iterate() {
    //call update to begin animation
    update(progress);
    if (percentage > 0) {
        //reduce count till it reaches 0
        percentage--;
        //increase progress

        progress += step;

        if (progress > 0.995) {
            progress = 0;
        }

        //Control the speed of the fill
        setTimeout(iterate, 10);
    }
}