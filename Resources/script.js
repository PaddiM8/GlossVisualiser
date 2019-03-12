document.getElementById("glossbox").addEventListener('mouseover', function(evt) {
   if (evt.target.className == "morpheme") {
      var tooltip = document.getElementById("tooltip");
      tooltip.style.display = "block";

      var tooltipText = evt.target.getAttribute("gloss");
      if (tooltipText == tooltipText.toUpperCase())
         tooltipText = abbreviations[tooltipText];

      var labels = evt.target.getAttribute("labels").split(' ');
      var i;
      for (i in labels) {
         if (labels[i] != "")
            tooltipText += "<br />" + abbreviations[labels[i]];
      }

      tooltip.innerHTML = tooltipText;

      var evtRect = evt.target.getBoundingClientRect();
      var left = evtRect.left - tooltip.getBoundingClientRect().width / 4;
      var top = evtRect.top + 38;
      var arrow = document.getElementById("tooltip-arrow");
      var arrowHeight = 14;
      var arrowWidth = 16;
      arrow.style.display = "block";

      if (left < 0) left = 0;
      tooltip.style.left = String(left) + "px";
      tooltip.style.top  = String(top) + "px";
      arrow.style.left = String(evtRect.left + evtRect.width / 2 - arrowWidth / 2) + "px";
      arrow.style.top  = String(top - arrowHeight) + "px";
   }
});

document.getElementById("glossbox").addEventListener('mouseleave', function(evt) {
   document.getElementById("tooltip").style.display = 'none';
   document.getElementById("tooltip-arrow").style.display = 'none';
});
