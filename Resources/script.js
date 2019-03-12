document.getElementById("glossbox").addEventListener('mouseover', function(evt) {
   if (evt.target.className == "morpheme") {
      var tooltip = document.getElementById("tooltip");
      tooltip.style.display = "block";

      var tooltipText = evt.target.getAttribute("gloss");
      if (tooltipText == tooltipText.toUpperCase())
         tooltipText = abbreviations[tooltipText];

      tooltip.innerHTML = tooltipText;

      var evtRect = evt.target.getBoundingClientRect();
      tooltip.style.left = String(evtRect.left - tooltip.getBoundingClientRect().width / 4) + "px";
      tooltip.style.top  = String(evtRect.top + 21) + "px";
   }
});

document.getElementById("glossbox").addEventListener('mouseleave', function(evt) {
   document.getElementById("tooltip").style.display = 'none';
});

