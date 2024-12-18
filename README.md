# circuit-calculator-sdk

The SDK version of the popular Experimenter's Station. Sources are [here](https://github.com/cartheur/circuitcalculator). In this version:

* Framework Windows Forms are console runtime of equations in predefined _solution pathways_ that can aid algorithmic development within the confines of CoPilot.
* While this, unfortuntaely, obscures what equations are executing, visibility will be experimented in various ways with interpretive logging, similar to AGI Chat feedback.
* The precision of computation is the aim of the project going forward. A reliable means to transform data into information without external influences.

## The Electromagnetic Circuit Calculator - Experimenter's Laboratory

A work-in-progress trying to compute the theoretical operational parameters of an electromagnetic circuit.

I started this project back in 2010, in order to capture the plethora of details which consist a simple wireless-powered circuit, like the one shown in a figure located in the data folder of this repository.

This is a Windows desktop application. This project was a big help in that I was able to come to terms with wireless power as a phenomenon and be able to organize the details sufficiently in order to generate publications. It is very handy, as an experimenter, to have a program which can store and compute the most interesting values for a set of transmitting coils in various configurations and dimensions. It aids the experimenter by freeing the mind from the minutia, allowing you to plainly see the meaning inherent in the design.

It can compute self-inductance and mutual inductance of loop coils, the quality of distant coils, the resonance properties of a circuit, the total circutal efficiency, the power ratio, and much more.

It is about 85% complete and needs love in a few places.

### Present workloads

* A computational engine for [pettable](https://github.com/Cartheur-Research/animals-pettable)
* What computations need to be added and how do we delete the SaaS approach denoted by Nadella?

### The containerized build

`podman build --force-rm --tag calculon:20241218 /home/cartheur/ame/aiventure/aiventure-github/cartheur/circuit-calculator-sdk`

#### Errata

* Branch _stale_ is the first-draft codebase.
