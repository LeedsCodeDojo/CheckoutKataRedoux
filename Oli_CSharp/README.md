C# / O-O Approach
=================

I had a brainwave after our drinks, and threw this solution together. Based on the solution Kiran and I worked on at the dojo, but with a fundamental change in the way discount rules are defined and applied.

Hopefully a mix of O-O and functional: *Any* type of discount can now be defined through the implementation of a IDiscountRule interface. The interface allows the rule to perform it's own search over the items collection, and look for matches.

See you all next time :)
